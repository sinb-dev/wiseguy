<template>
  <div>
      <h1>Issue </h1>
      <table>
          <tr><th>Name</th><th>email</th><th>token</th><th>link</th></tr>
          <tr   v-for="copy in issue.copies"
                v-bind:key="copy.email">
                    <td>{{copy.name}}</td>
                    <td>{{copy.email}}</td>
                    <td>{{copy.token}}</td>
                    <td><a :href="getlink(copy.token)">link</a></td>
                </tr>
      </table>
  </div>
</template>

<script>
export default {
    data() {
        return {issue : {
            copies : []
        }}
    },
    methods : {
        getlink(token) {
            return "#/eval/"+token;
        }
    },
    mounted() {
        if (this.$root.pageData != null) {
            this.issue = this.$root.pageData;
        } else {
            var issueId = this.$root.folder(2);
            if (isNaN(issueId) || !issueId) {
                console.error("missing issue id")
                return;
            }
            this.$root.get("issue/"+issueId+"/copies").then(r => this.issue = r.data);
        }
    }
}
</script>

<style>

</style>