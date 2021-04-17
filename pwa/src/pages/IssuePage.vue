<template>
  <div>
      <h1>Issue {{issue.course}} > {{issue.subject}}</h1>
      <table>
          <tr>
              <th>Name</th>
              <th>email</th>
              <th>token</th>
              <th>link</th>
              <th>udfyldt</th>
              <th>Other issues</th>
            </tr>
          <tr   v-for="copy in issue.copies"
                v-bind:key="copy.email">
                    <td>{{copy.name}}</td>
                    <td>{{copy.email}}</td>
                    <td>{{copy.token}}</td>
                    <td><a :href="getlink(copy.token)">link</a></td>
                    <td>{{copy.completed}}</td>
                    <td><md-button  v-for="other in copy.otherIssues"
                                    :key="other"
                                    :href="issurl(other)">{{other}}</md-button></td>
                </tr>
      </table>
      <md-button href="/#/">Back</md-button>
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
        },
        issurl(id) {
        return "#/issue/"+id;
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