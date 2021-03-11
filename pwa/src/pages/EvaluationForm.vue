<template>
  <div>Evaluation form
      <div><strong>Fag: </strong>{{course}}</div>
      <div><strong>Emne: </strong>{{subject}}</div>
      <div>Hvor godt kender du disse ord?</div>
      <table >
          <tr><th class="phrase">Ord</th><th class="field">Kender ikke</th><th class="field">Hørt om</th><th class="field">Kan forklare</th></tr>
      <evaluation-phrase    v-for="phrase in phrases"
                            v-bind:phrase="phrase.text"
                            v-bind:id="phrase.id"
                            v-bind:key="phrase.id"
                            @cross="oncross"></evaluation-phrase>
        </table>
        <input type="button" @click="submit" value="send" />
  </div>
</template>

<script>
import EvaluationPhrase from '../components/EvaluationPhrase.vue';
import URL from '../scripts/url.js';

export default {
    components : {
        'evaluation-phrase' : EvaluationPhrase
    },
    data() {
        return {
            course : "",
            subject : "",
            phrases : []
        };
    },
    methods : {
        oncross(data) {
            for (var i in this.phrases) {
                if (this.phrases[i].id == data.id) {
                    this.phrases[i].answer = data.value;
                }
            }
        },
        submit() {
            var packet = {
                sender : URL.workingDirectory(),
                answer : this.phrases,
            }
            console.log(packet);
        },
        load(data) {
            console.log(data);
            this.course = data.course;
            this.subject = data.subject;
            this.phrases = data.phrases;
        }
    },
    mounted() {
        var token = this.$root.folder(2);
        this.$root.get("eval/"+token)
            .then(reponse => self.load(reponse.data))
    }
}
</script>

<style>
    table {
        width: 60%
    }
    th.phrase {
        width: 40%;
    }
    th.field  {
        width: 20% !important;
    }
</style>