<template>
  <div>Evaluation form
        
        <div v-if="!editMode(completed)" class="legend">
        <evaluation-legend v-for="row in dates"
            :key="row.symbol"
            :date="row.date"
            :symbol="row.symbol"></evaluation-legend>
        </div>
        <div><strong>Fag: </strong>{{course}}</div>
        <div><strong>Emne: </strong>{{subject}}</div>
        <div>Hvor godt kender du disse ord?</div>
      <table >
          <tr><th class="phrase">Ord</th><th class="field">Kender ikke</th><th class="field">Hørt om</th><th class="field">Kan forklare</th></tr>
      <evaluation-phrase    v-for="phrase in phrases"
                            v-bind:phrase="phrase.text"
                            v-bind:id="phrase.id"
                            v-bind:key="phrase.id"
                            :edit="editMode(completed)"
                            :answers="phrase.answers"
                            @cross="oncross"></evaluation-phrase>
        </table>

        <md-button v-if="editMode(completed)" @click="submit">Send</md-button>
  </div>
</template>

<script>
import EvaluationPhrase from '../components/EvaluationPhrase.vue';
import EvaluationResultLegend from '../components/EvaluationResultLegend.vue';
import Access from '../scripts/Access.js';
import URL from '../scripts/url.js';

export default {
    components : {
        'evaluation-phrase' : EvaluationPhrase,
        'evaluation-legend' : EvaluationResultLegend
    },
    data() {
        return {
            course : "",
            subject : "",
            completed : "",
            phrases : [],
            answers : {},
            answersByDate : {},
            symbolsByDate : {}
        };
    },
    computed : {
        dates() {
            var list = [];
            for(var date in this.answersByDate) {

                list.push({
                    'date' : new Date(date),
                    symbol : this.symbolsByDate[date]
                });
            }
            
            return list;
        }
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
            var phrases = new Array();
            for (var i in this.phrases) {
                phrases[i] = {
                    id : this.phrases[i].id,
                    answer : this.phrases[i].answer,
                    text : this.phrases[i].text
                }
                if (!this.phrases[i].answer) {
                    console.error("Not all words have been checked")
                    return false;
                }
            }

            var packet = {
                sender : URL.workingDirectory(),
                answers : phrases,
            }

            var token = this.$root.folder(2);

            this.$root.post("eval/"+token, packet);
        },
        load(data) {
        
            this.answersByDate = {};
            var symbols = ["X","O","#","@","&","$"];
            
            var i = 0;

            //Maps answers by date for the legend (and finding symbol)
            for (let phraseId in data.answers) {
                let answers = data.answers[phraseId];

                answers.forEach(answer=>{
                    if (typeof (this.answersByDate[answer.completed]) == "undefined") {
                        this.answersByDate[answer.completed] = {};
                        this.symbolsByDate[answer.completed] = symbols[i++];
                    }
                    
                    this.answersByDate[answer.completed][phraseId] = answer.answer;
                })
            }

            //Maps answers by phrase for generating responses
            var phrases = [];
            for (let i in data.phrases) {
                var p = {
                    id : data.phrases[i].id,
                    text : data.phrases[i].text,
                    answers : {
                    }
                }

                for (let date in this.answersByDate) 
                {
                    if (this.answersByDate[date][p.id]) 
                    {
                        if (!p.answers[ this.answersByDate[date][p.id]]) {
                            p.answers[ this.answersByDate[date][p.id] ] = []
                        }
                        p.answers[ this.answersByDate[date][p.id] ].push(this.symbolsByDate[date]);
                    }
                }
                phrases.push(p);
            }
            
            Access.load();
            if (data.access && this.$root.adminMode == false) {
                Access.add(data.access);
            }

            this.course = data.course;
            this.subject = data.subject;
            this.phrases = phrases//data.phrases;
            this.completed = data.completed;
        },
        isComplete() {
            var date = new Date(this.completed)
            return date.getFullYear() != 1
        },
        editMode() {
            var date = new Date(this.completed)
            return date.getFullYear() == 1
        }
    },
    mounted() {
        var token = this.$root.folder(2);
        this.$root.get("eval/"+token)
            .then(reponse => this.load(reponse.data))
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
    div.legend {
        float:right;
    }
</style>