<template>
    <div>
        <edit-template 
            v-bind:id="templateId" 
            v-bind:course="template.course"
            v-bind:subject="template.subject"
            v-bind:phraseText="phraseText"
            v-on:update="load"></edit-template><br>
        <md-content>
            <md-button class="md-primary" @click="update">Save template</md-button>
            <md-button @click="issue" title="Create copies and send to students for filling"
                v-if="templateId != 0" >
                Issue copies to list
            </md-button>
        </md-content>

        <div class="ui basic modal">
            
            <md-dialog :md-active.sync="show_issue_modal">
                <issue-modal    
                        v-bind:lists="maillists" 
                        v-bind:templateId="templateId"
                        @issued="templateIssued"></issue-modal>

  
            </md-dialog>

    <!--<md-button class="md-primary md-raised" @click="showDialog = true">Show Dialog</md-button>-->
            
        </div>
    </div>
</template>

<script>
import EditTemplate from '../components/EditTemplate.vue';
import IssueTemplateToMaillist from '../components/IssueTemplateToList.vue';

export default {
  components: { 
        'edit-template' : EditTemplate,
        'issue-modal' : IssueTemplateToMaillist 
    },
    
    data() {
        return {
            templateId : 0,
            template : {
                subject : "",
                course : "",
                phrases : []
            },
            show_issue_modal : false,
            maillists : []
        }
    },
    methods : {
        load(data) {
            this.template = data;
        },
        update() {
            var data = new FormData();
            data.append("Id", this.templateId);
            data.append("Course", this.template.course);
            data.append("Subject", this.template.subject);
            
            this.template.phrases.forEach(
                p => data.append("Phrases", p)
            )

            var op = this.templateId > 0 ? "tpl/update" : "tpl/new";
            this.$root
                .post( op, data)
                .then(d => this.templateId == 0 ? this.templateId = d.data.id : 0);
        },
        issue : function() {
            this.show_issue_modal = true;
            //let $ = window.jQuery;
            //$(".ui.basic.modal").click(function() {$(".ui.basic.modal").modal("hide")}).modal("show");
            
        },
        templateIssued : function(data) {
            //User issued this template, take him to the list of copies
            this.$root.go("issue/"+data.issueId, data);
        }
    },
    computed : {
        phraseText() {
            return this.template.phrases.join("\n");
        }
    },
    mounted() {
        this.templateId = parseInt(this.$root.folder(2)) || 0;
        if (this.templateId > 0) {
           this.$root
            .get('tpl/'+this.templateId)
            .then(reponse => this.load(reponse.data))

            this.$root.get("list/lists").then( response => this.maillists = response.data);
        }
    }
}
</script>

<style>

</style>