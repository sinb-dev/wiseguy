<template>
    <div>
        <edit-list 
            v-on:update="load"
            :name="list.name"
            :emailText="emailsText"></edit-list>

        <md-button class="md-secondary" href="#/admin/">Back</md-button>
        <md-button @click="update">Save template</md-button>
    </div>
</template>

<script>
//import PouchDB from 'pouchdb';
import EditList from '../components/EditMaillist.vue';
export default {
    components : {
        'edit-list' : EditList
    },
    data() {
        return {
            listId : 0,
            list : {
                name : "",
                emails : []
            }
        }
    },
    methods : {
        load(data) {
            this.list = data;
        },
        update() {
            var data = new FormData();
            data.append("Id", this.listId);
            data.append("Name", this.list.name);
            console.log(data);
            
            this.list.emails.forEach(
                p => data.append("Emails", p)
            )
            
            var self = this;
            var op = this.listId > 0 ? "list/update" : "list/new";
            var method = this.listId > 0 ? this.$root.put : this.$root.post;
            method( op, data)
                .then(d => {
                    self.listId == 0 ? self.listId = d.data.id : 0;
                    self.$root.onMaillistUpdate({ "id": self.listId, "name": self.list.name} );
                    self.$root.message("Mail list saved successfully");
                })
                .catch(m => self.$root.message("Error: "+m));
        }
    },
    computed : {
        emailsText() {
            return this.list.emails.join("\n");
        }
    },
    mounted() {
        this.listId = parseInt(this.$root.folder(2)) || 0;
        if (this.listId > 0) {
           this.$root
            .get('list/'+this.listId)
            .then(reponse => this.load(reponse.data))
        }
    }
}
</script>

<style>

</style>