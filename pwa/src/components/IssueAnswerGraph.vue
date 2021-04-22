<template>
  <div>
      <issue-answer-graph-bar v-for="issue in issues" 
        :key="issue.id" :color="getcolor(issue.issueId)" :classnames="getclass(issue.count)">
      </issue-answer-graph-bar>
    </div>
</template>

<script>
import IssueAnswerGraphBar from './IssueAnswerGraphBar.vue';
export default {
  components: { IssueAnswerGraphBar },
    props: {
        issues : Array,
        max : Number,
        issuecolors : Object
    },
    methods: {
        getclass(count) {
            var s = Math.ceil(count/this.max*10);
            return "issueAnswer s"+s
        },
        getcolor(issueId) {
            
            if (this.issuecolors[issueId])
                return this.issuecolors[issueId]
            return "#cccccc";
        }
    }
}
</script>

<style>
span.issueAnswer  {
    width:10px;
    background:red;
    display:inline-block;
    vertical-align:bottom;

}
span.issueAnswer.s0 { height:0px; }
span.issueAnswer.s1 { height:2px; }
span.issueAnswer.s2 { height:4px; }
span.issueAnswer.s3 { height:6px; }
span.issueAnswer.s4 { height:8px; }
span.issueAnswer.s5 { height:10px; }
span.issueAnswer.s6 { height:12px; }
span.issueAnswer.s7 { height:14px; }
span.issueAnswer.s8 { height:16px; }
span.issueAnswer.s9 { height:18px; }
span.issueAnswer.s10 { height:20px; }
</style>