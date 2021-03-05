<template>
    <div>
        <edit-template 
            v-bind:id="templateId" 
            v-bind:course="course"
            v-bind:subject="subject"
            v-bind:phraseText="phraseText"
            v-on:update="templateUpdated"></edit-template>
        <button @click="update">Save template</button>
    </div>
</template>

<script>
import EditTemplate from '../components/EditTemplate.vue';
export default {
  components: { 'edit-template' : EditTemplate },
    
    data() {
        return {
            templateId : 2,
            subject : "",
            course : "",
            phrases : []
        }
    },
    methods : {
        load(data) {
            this.course = data.course;
            this.subject = data.subject;
            this.phrases = data.phrases;
            console.log("TemplateManager: "+data.course);
        },
        templateUpdated(data) {
            this.templateData = data;
        },
        update() {
            var data = new FormData();
            data.append("id", this.templateId);
            data.append("course", this.templateData.course);
            data.append("subject", this.templateData.subject);
            
            this.templateData.phrases.forEach(
                p => data.append("phrases", p)
            )

            /*this.$root
             .post( "tpl/update", data)
             .then(d => console.log(d));*/
        }
    },
    computed : {
        phraseText() {
            return this.phrases.join("\n");
        }
    },
    mounted() {
        this.templateId = 2;
        if (this.templateId > 0) {
           this.$root
            .get('tpl/'+this.templateId)
            .then(reponse => this.load(reponse.data))
        }
    }
}
</script>

<style>

</style>