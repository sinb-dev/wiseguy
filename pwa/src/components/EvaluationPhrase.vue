<template>
  <tr>
    <td>{{phrase}}</td>
    <td v-if="edit"><input type="button" :id="field1" @click="cross" ></td>
    <td v-if="edit"><input type="button" :id="field2" @click="cross"></td>
    <td v-if="edit"><input type="button" :id="field3" @click="cross"></td>
    <td v-if="!edit" class="answer">
      <b v-for="a in answers['1']"
      :key="a.id">{{a}}</b>
    </td>
    <td v-if="!edit" class="answer">
      <b v-for="a in answers['2']"
      :key="a.id">{{a}}</b>
    </td>
    <td v-if="!edit" class="answer">
      <b v-for="a in answers['3']"
      :key="a.id">{{a}}</b>
    </td>
  </tr>
</template>

<script>
export default {
    props : {
        phrase : String,
        id : Number,
        edit : Boolean,
        answers : Object
    },
    computed : {
      field1() {return "evaluationfield-"+this.id+"-1";},
      field2() {return "evaluationfield-"+this.id+"-2";},
      field3() {return "evaluationfield-"+this.id+"-3";},
    },
    methods : {
      cross(event) {
        let id = parseInt(event.target.id.split("-")[1]);
        let number = parseInt(event.target.id.split("-")[2])
        if (number < 1 || number > 3) return console.error("Invalid checkbox number");
        this.clearAll()
        event.target.value = "X"
        this.$emit("cross",{id: id, value : number})
      },
      clearAll() {
        document.getElementById(this.field1).value = "";
        document.getElementById(this.field2).value = "";
        document.getElementById(this.field3).value = "";
      }
    },
    mounted() {

    }
}
</script>

<style>
  input {
    width:100%;
    height:32px;
  }
  div.answer {
    text-align:center;
  }
  td.answer {
    text-align:center;
  }
</style>