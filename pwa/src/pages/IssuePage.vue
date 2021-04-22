<template>
  <div>
      <md-content class="md-elevation-1" style="padding:16px">
        <span class="md-headline">Issue {{issue.course}} > {{issue.subject}}</span>
      </md-content>
      <br>
      
      <issue-performance v-if="issueId != '0'" :status="status" :issueId="issueId"></issue-performance>
      <br>
      <md-content class="md-elevation-1">
      <md-table>
          <md-table-row>
              <md-table-head>Name</md-table-head>
              <md-table-head>email</md-table-head>
              
              <md-table-head>link</md-table-head>
              <md-table-head>udfyldt</md-table-head>
              <md-table-head>Other issues</md-table-head>
            </md-table-row>
          <md-table-row   v-for="copy in issue.copies"
                v-bind:key="copy.email">
                    <md-table-cell>{{copy.name}}</md-table-cell>
                    <md-table-cell>{{copy.email}}</md-table-cell>
                    <md-table-cell><md-button :href="getlink(copy.token)">view</md-button></md-table-cell>
                    <md-table-cell>{{copy.completed}}</md-table-cell>
                    <md-table-cell><md-button  v-for="other in copy.otherIssues"
                                    :key="other"
                                    :href="issurl(other)">{{other}}</md-button></md-table-cell>
                </md-table-row>
      </md-table>
      </md-content>
      <md-content class="md-elevation-1">
        <md-button href="/#/">Back</md-button>
      </md-content>
  </div>
</template>

<script>
import IssuePerformance from '../components/IssuePerformance.vue';
export default {
    components : {
        IssuePerformance
    },
    data() {
        return {
            issue : {
                copies : [],
            },
            issueId : "0",
            status : {
                latest_submission : "2021-04-20T12:00:00",
                phrases : [
                    {   phrase : "Inner join", id:1,
                        answers : {
                            "1" : [ //answer type
                                { issueId : 2001, count : 5 }, //issueId and number of answer with type 1
                                { issueId : 2003, count : 3 },
                            ],
                            "2" : [ 
                                { issueId : 2001, count : 5 }, //issueId and number of answer with type 2
                                { issueId : 2003, count : 3 },
                            ],
                            "3" : [ 
                                { issueId : 2001, count : 5 },
                                { issueId : 2003, count : 3 },
                            ],
                        }
                    },
                    {   phrase : "Exists join", id:2,
                        answers : {
                            "1" : [ //answer type
                                { issueId : 2001, count : 5 }, //issueId and number of answer with type 1
                                { issueId : 2003, count : 3 },
                            ],
                            "2" : [ 
                                { issueId : 2001, count : 5 }, //issueId and number of answer with type 2
                                { issueId : 2003, count : 3 },
                            ],
                            "3" : [ 
                                { issueId : 2001, count : 5 },
                                { issueId : 2003, count : 3 },
                            ],
                        }
                    }
                ]
            }}
    },
    methods : {
        getlink(token) {
            return "#/eval/"+token;
        },
        issurl(id) {
        return "#/issue/"+id;
        }
    },
    mounted() {
        if (this.$root.pageData != null) {
            this.issue = this.$root.pageData;
        } else {
            this.issueId = this.$root.folder(2);
            if (isNaN(this.issueId) || !this.issueId || this.issueId == "0") {
                console.error("missing issue id")
                return;
            }
            
            this.$root.get("issue/"+this.issueId+"/copies").then(r => this.issue = r.data);
        }
    }
}
</script>

<style>

</style>