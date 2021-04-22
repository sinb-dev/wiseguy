<template>
  <div>
      <div v-if="copies.length ==0" style="text-align:center;margin-top:50px">
        <md-empty-state
          md-rounded
          
          md-label="Der er hul igennem"
          md-description="Hæng i, du får et link til et ark, du skal udfylde">
        </md-empty-state>
        
        </div>
      <div v-else>
        <md-content style="margin-top:16px;padding:8px;" class="md-elevation-1">
          <span class="md-display-3">Hej {{name}}</span>
        </md-content>
        
        <md-content style="margin-top:16px;padding:8px;" class="md-elevation-1">
          <div class="md-layout">
            <div class="md-layout-item header">Skabelon</div>
            <div class="md-layout-item header">Dannet</div>
            <div class="md-layout-item header">Udfyldt</div>
          </div>
          <div class="md-layout" style="height:36px" v-for="copy in copies"
              :key="copy.token">
            <div class="md-layout-item" style="margin-top:auto;margin-bottom:auto">{{copy.course}} > {{copy.subject}}</div>
            <div class="md-layout-item" style="margin-top:auto;margin-bottom:auto">{{date_formatted(copy.date)}} {{time_formatted(copy.date)}}</div>
            <div class="md-layout-item">
              <md-button class="md-dense md-raised md-primary" :href="copy_link(copy.token)" v-if="copy.completed.getFullYear() == '0001'">Udfyld arket</md-button>
              <md-button :href="copy_link(copy.token)" v-else>{{date_formatted(copy.completed)}} {{time_formatted(copy.completed)}}</md-button>
            </div>
          </div>
        </md-content>
      </div>
  </div>
</template>

<script>
import Access from '../scripts/Access.js'
export default {
  data() {
    return {
      name : "",
      copies : []
    }
  },
  methods : {
    date_formatted(dateStr) {
        var date = new Date(dateStr);
        return date.getDate()+"/"+date.getMonth()+"/"+date.getFullYear();
    },
    time_formatted(dateStr) {
        var date = new Date(dateStr);
        var hrs = date.getHours();
        if (hrs < 10) 
          hrs = "0"+hrs;
        var mins = date.getMinutes();
        if (mins < 10) 
          mins = "0"+mins;
        return "kl. "+hrs+":"+mins;
    },
    copy_link(token) {
      return "/#/eval/"+token;
    }
  },
  mounted() {
    //Get all templates this user is involved in
    var tokens = Access.get_accessTokens();
    console.log(tokens);
    console.log("Participant/copies/"+tokens[0]);
    var self = this;
    tokens.forEach( token => 
      this.$root.get("Participant/copies/"+token)
        .then(response => {
          let data = response.data;

          self.copies = data.copies;
          self.name = data.name;
          for (let i in self.copies) { 
            self.copies[i].date = new Date(self.copies[i].date)
            self.copies[i].completed = new Date(self.copies[i].completed)
          }
        })
        .catch()
    )
  }
}
</script>

<style>
div.header {
  font-size: 110%;
  font-weight:bold;
}
</style>