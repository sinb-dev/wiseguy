<template>
  <div>
      <h1 class="ui dividing header">Wise Guy - Learning Evaluation</h1>
      <a href="#/tpl/">Create new sheet</a><br>
      <a href="#/list/">Create list of recipients</a>

      <div>Recent templates</div>
      <ol>
        <li v-for="r in $root.recent.templates"
            v-bind:key="r.id"><a v-bind:href="tplurl(r.id)">{{r.course}} > {{r.subject}}</a></li>
      </ol>
      <div>Recent lists</div>
      <ol>
        <li v-for="r in $root.recent.lists"
            v-bind:key="r.id"><a v-bind:href="lsturl(r.id)">{{r.name}}</a></li>
      </ol>
  </div>
</template>

<script>
export default {
  methods : {
    tplurl(id) {
      return "#/tpl/"+id; 
    },
    lsturl(id) {
      return "#/list/"+id;
    }
  }
}
</script>

<style>

</style>