<template>
  <div>
      <h1 v-if="course==''">New template</h1>
      <h1 v-else>Edit {{course}} > {{subject}}</h1>
            Course <input type="text" id="txtCourse" name="course" :value="course" @change="update" /><br>
      Subject <input type="text" id="txtSubject" name="subject" :value="subject" @change="update" /><br>
      <label>Terms</label><br>
      <textarea :value="phraseText" v-on:change="update" id="txtPhrases" ></textarea>
      
  </div>
</template>

<script>
function val(id) {
    return document.getElementById(id).value;
}
export default {
    props : {
        "course" : String,
        "subject" : String,
        "phraseText" : String,
    },
    methods : {
        update() {
            var phrases = [];
            var tmp = val("txtPhrases").split("\n");
            
            for (var i in tmp) {
                var trimmed = tmp[i].trim();
                if (trimmed != "")
                    phrases.push(trimmed);
            }
            
            this.$emit("update", {
                course : val('txtCourse'),
                subject : val('txtSubject'),
                phrases : phrases
            })
        }
    }
}
</script>

<style>

</style>