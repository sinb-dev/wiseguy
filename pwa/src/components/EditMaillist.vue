<template>
  <div>
      <h1 v-if="name==''">New mail list</h1>
      <h1 v-else>Edit {{name}}</h1>
            Name <input type="text" id="txtName" name="name" :value="name" @change="update" /><br>
      <label>Emails</label><br>
      <textarea :value="emailText" v-on:change="update" id="txtEmails" ></textarea>
      
  </div>
</template>

<script>
function val(id) {
    return document.getElementById(id).value;
}
export default {
    props : {
        "name" : String,
        "emailText" : String,
    },
    methods : {
        update() {
            var emails = [];
            var tmp = val("txtEmails").split("\n");
            
            for (var i in tmp) {
                var trimmed = tmp[i].trim();
                if (trimmed != "")
                    emails.push(trimmed);
            }
            
            this.$emit("update", {
                name : val('txtName'),
                emails : emails
            })
        },
        mounted() {
            //this.$root.
        }
    }
}
</script>

<style>

</style>