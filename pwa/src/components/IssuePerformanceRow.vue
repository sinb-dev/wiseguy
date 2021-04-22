<template>
  <md-table-row >
        <md-table-cell>{{phraseperformance.phrase}}</md-table-cell>
        <md-table-cell v-for="issue in phraseperformance.answers" :key="issue.id">
            
            <issue-answer-graph :max="recipients" :issues="issue" :issuecolors="issuecolors"></issue-answer-graph>
        </md-table-cell>
    </md-table-row>
</template>

<script>
import IssueAnswerGraph from './IssueAnswerGraph.vue'
export default {
  components: { IssueAnswerGraph },
    props : {
        phraseperformance : Object,
        issuecolors : Object,
        recipients : Number
    }
}
</script>

<style>

</style>