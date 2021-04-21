<template>
  <div>
      <md-content class="md-elevation-1" style="padding:16px">
        <span class="md-headline">Issue {{issue.course}} > {{issue.subject}}</span>
      </md-content>
      <br>
      <md-content class="md-elevation-1" style="padding:16px">
        <span class="md-subheading">Deltagernes kendskab</span>
        <md-table>
            <md-table-row>
              <md-table-head>Ord</md-table-head>
              <md-table-head>Kender ikke</md-table-head>
              <md-table-head>Har hørt om</md-table-head>
              <md-table-head>Kan forklare</md-table-head>
            </md-table-row>
            <md-table-row v-for="phrase in status.phrases" :key="phrase.id">
                <md-table-cell>{{phrase.phrase}}</md-table-cell>
                <md-table-cell v-for="answers in phrase.answers" :key="answers">{{answers}}</md-table-cell>
            </md-table-row>
        </md-table>
      </md-content>
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

export default {
    data() {
        return {
            issue : {
                copies : [],
            
        },
        status : {
                latest_submission : "2021-04-20T12:00:00",
                phrases : [
                    {   phrase : "Inner join",
                        answers : {
                            "1" : 5,
                            "2" : 3,
                            "3" : 4
                        }
                    },
                    {   phrase : "Exists join",
                        answers : {
                            "1" : 7,
                            "2" : 2,
                            "3" : 1
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