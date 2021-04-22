<template>
  <md-content class="md-elevation-1" style="padding:16px">
    <span class="md-subheading">Deltagernes kendskab</span>
    
    <md-table>
        <md-table-row>
            <md-table-head>Ord</md-table-head>
            <md-table-head>Kender ikke</md-table-head>
            <md-table-head>Har hørt om</md-table-head>
            <md-table-head>Kan forklare</md-table-head>
        </md-table-row>
        
        <issue-performance-row v-for="phrase in status.phrases" :key="phrase.id" :recipients="status.recipients" :phraseperformance="phrase" :issuecolors="issuesAndColors"></issue-performance-row>

    </md-table>
    </md-content>
</template>

<script>
import IssuePerformanceRow from './IssuePerformanceRow.vue'
export default {
  components: { IssuePerformanceRow },
    props : {
        //status: Object,
        issueId : String
    },
    data : function() {
        return {
            status : {}
        }
    },
    computed : {
        issuesAndColors() {
            let colors = ["#caecff", "#acd0f9", "#85abef", "#5c85e7", "#012fd3"]
        let c = 0;
            let issues = {};
            
            for (let i in this.status.phrases) {
                for (let answerType in this.status.phrases[i].answers) {
                    for (let x in this.status.phrases[i].answers[answerType]) {
                        let issueId = this.status.phrases[i].answers[answerType][x].issueId;
                        if (!issues[issueId])
                            issues[issueId] = colors[c++];
                    }
                }
            }
            return issues;
        }
    },
    mounted() {
        
       
        if (isNaN(this.issueId) || !this.issueId || this.issueId == "0") {
            console.error("missing issue id")
            return;
        }
        this.$root.get("issue/"+this.issueId+"/performance")
            .then(response => {
                this.status = response.data
                
            });
    }   
}
</script>

<style>

</style>