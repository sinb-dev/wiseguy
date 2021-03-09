<template>
    <div>
        <edit-list 
            v-on:update="load"
            :name="list.name"
            :emailText="emailsText"></edit-list>
        <button @click="update">Save template</button>
    </div>
</template>

<script>
import EditMaillist from '../components/EditMaillist.vue';
export default {
  components: { 'edit-list' : EditMaillist },
    
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
            console.log(data);
            this.list = data;
        },
        update() {
            var data = new FormData();
            data.append("Id", this.listId);
            data.append("Name", this.list.name);
            
            this.list.emails.forEach(
                p => data.append("Emails", p)
            )
            
            var op = this.listId > 0 ? "list/update" : "list/new";
            
            this.$root
                .post( op, data)
                .then(d => console.log(d));
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