<template>
<div>
  <select v-for="list in maillists"
    :key="list.na">{{list.name}}</select>
    <button @click="issue">Issue</button>
</div>
</template>

<script>
export default {
    props : {
        maillists : Array,
        templateId : Number
    },
    methods : {
        issue() {
            //this.$root.
        }
    }
}
</script>

<style>

</style>