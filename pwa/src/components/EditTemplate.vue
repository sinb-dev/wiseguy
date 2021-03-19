<template>
    <div>
        <h1 v-if="course==''" class="ui dividing header">New template</h1>
        <h1 v-else class="ui dividing header">Edit {{course}} > {{subject}}</h1>

        <div class="ui form">
            <div class="field">
                <label>Course</label>
                <input type="text" id="txtCourse" name="course" :value="course" @change="update"/>
            </div>
            <div class="field">
                <label>Subject</label>
                <input type="text" id="txtSubject" name="subject" :value="subject" @change="update" />
            </div>
            <div class="ui ignored positive message">Phrases are seperated by each line</div>
            <div class="field">
                <label>Terms</label>
                <textarea :value="phraseText" v-on:change="update" id="txtPhrases" ></textarea>
            </div>
        </div>
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