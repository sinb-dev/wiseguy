<template>
  <div>
      <span v-for="answer in issues" 
        :key="answer" :class="getclass(answer)">
      </span>
    </div>
</template>

<script>
export default {
    props: {
        issues : Array,
        max : Number
    },
    methods: {
        getclass(count) {
            
            var s = parseInt(count/this.max*10);
            return "issueAnswer s"+s
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
</style>