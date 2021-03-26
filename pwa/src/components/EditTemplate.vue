<template>
    <div>
        <md-content>
        <h1 v-if="course==''" class="ui dividing header">New template</h1>
        <h1 v-else class="ui dividing header">Edit {{course}} > {{subject}}</h1>

        <div class="ui form">
            <md-field>
                <label>Course</label>
                <md-input type="text" id="txtCourse" name="course" :value="course" @change="update"/>
            </md-field>
            <md-field>
                <label>Subject</label>
                <md-input type="text" id="txtSubject" name="subject" :value="subject" @change="update" />
            </md-field>
            <div class="ui ignored positive message">Phrases are seperated by each line</div>
            <md-field>
                <label>Terms</label>
                <md-textarea :value="phraseText" v-on:change="update" id="txtPhrases" md-autogrow ></md-textarea>
            </md-field>
        </div>
        </md-content>
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