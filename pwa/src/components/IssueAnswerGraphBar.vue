<template>
  <span :class="classnames" :style="styles()">
      </span>
</template>

<script>
export default {
    props : {
        classnames : String,
        color : String
    },
    methods : {
        styles() {
            return "background-color: "+this.color;
        }
    }
}
</script>

<style>

</style>