<template>
  <div>
      <h1 v-if="course==''">New template</h1>
      <h1 v-else>Edit {{course}} > {{subject}}</h1>
      <input type="hidden" name="id" v-model="hdnId" />
      Course <input type="text" name="course" v-model="txtCourse" v-on:change="update" /><br>
      Subject <input type="text" name="subject"  v-model="txtSubject" v-on:change="update" /><br>
      <label>Terms</label><br>
      <textarea v-model="txtPhrases"></textarea>
      
  </div>
</template>

<script>
export default {
    props : {
        "id" : Number,
        "course" : String,
        "subject" : String,
        "phraseText" : String,
    },
    data() { return {
        txtCourse : this.course,
        txtSubject : this.subject,
        txtPhrases : this.phraseText,
        hdnId : this.id

    }},

    methods : {
        update() {
            var phrases = [];
            var tmp = this.txtPhrases.split("\n");
            for (var i in tmp) {
                phrases.push(tmp[i].trim());
            }
            
            this.$emit("update", {
                course : this.txtCourse,
                subject : this.txtSubject,
                phrases : phrases
            })
        }
    },
    mounted() {
        //this.update();
        // console.log(this.course);
        // this.txtCourse = this.course;
    }
}
</script>

<style>

</style>