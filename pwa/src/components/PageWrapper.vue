<template>
  <div>
      <component :is="component" v-if="authorized" :key="done"></component>
      <!--<login-page v-else ></login-page>-->
      
      <md-snackbar :md-active.sync="$root.DisplayMessage"><span>{{$root.UserMessage}}</span></md-snackbar>
  </div>
</template>

<script>

import Admin from '../pages/Admin.vue'
import EvaluationForm from '../pages/EvaluationForm.vue';
import IssuePage from '../pages/IssuePage.vue';
import ListManager from '../pages/ListManager.vue';
import NotFound from '../pages/NotFound.vue';
import StartPage from '../pages/StartPage.vue';
import TemplateManager from '../pages/TemplateManager.vue';
import LoginPage from '../pages/LoginPage.vue';
export default {
    data : () => {
        return {
            done: false,
            requiresLogin : ['Admin','IssuePage','ListManager','TemplateManager']
        }
    },
    methods : {
    },
    components: {
        'Admin' : Admin, 
        'EvaluationForm' : EvaluationForm, 
        'IssuePage' : IssuePage, 
        'ListManager' : ListManager, 
        'NotFound' : NotFound, 
        'StartPage' : StartPage, 
        'TemplateManager' : TemplateManager,
        'LoginPage' : LoginPage
    },
    computed: {
        component() {
            return this.$root.ViewComponent;
        },
        authorized() {
            var auth = this.requiresLogin.indexOf(this.$root.ViewComponent) == -1 || this.$root.isLoggedIn()
            if (!auth) this.$root.go("login");
            return auth;
        }
    },
    mounted() {
        console.log("Render pagewrapper")
    }
    
}
</script>

<style>

</style>