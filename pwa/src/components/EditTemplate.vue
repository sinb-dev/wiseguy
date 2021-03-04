<template>
  <div>
      <h1 v-if="course==''">New template</h1>
      <h1 v-else>Edit {{course}} > {{subject}}</h1>
      <input type="hidden" name="id" v-model="id" />
      Course <input type="text" name="course" v-model="course" v-on:change="update" /><br>
      Subject <input type="text" name="subject" v-model="subject" v-on:change="update" /><br>
      <label>Terms</label><br>
      <textarea v-model="phraseText"></textarea>
      
  </div>
</template>

<script>
export default {
    props : {
        "id" : Number,
        "course" : String,
        "subject" : String,
        "phraseText" : String
    },

    methods : {
        update() {
            var phrases = [];
            var tmp = this.phraseText.split("\n");
            for (var i in tmp) {
                phrases.push(tmp[i].trim());
            }

            this.$emit("update", {
                course : this.course,
                subject : this.subject,
                phrases : phrases
            })
        }
    }
}
</script>

<style>

</style>