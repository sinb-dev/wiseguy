<template>
  <div>
      <login-form :loggedIn="loggedIn"></login-form>
  </div>
</template>

<script>
import LoginForm from '../components/LoginForm.vue';
export default {
  components: { 
      'login-form' : LoginForm
  },
  methods: {
    loggedIn() {
        console.log("login page recived emit")
    }
  }
}
</script>

<style>

</style>