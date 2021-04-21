<template>
  <div>
      <component :is="component"></component>
      
      <md-snackbar :md-active.sync="$root.showUserMessage"><span>{{$root.UserMessage}}</span></md-snackbar>
  </div>
</template>

<script>

import Admin from '../pages/Admin.vue'
import EvaluationForm from '../pages/EvaluationForm.vue';
import IssuePage from '../pages/IssuePage.vue';
import ListManager from '../pages/ListManager.vue';
import NotFound from '../pages/NotFound.vue';
import StartPage from '../pages/StartPage.vue';
import TemplateManager from '../pages/TemplateManager';
export default {
    components: {
        'Admin' : Admin, 
        'EvaluationForm' : EvaluationForm, 
        'IssuePage' : IssuePage, 
        'ListManager' : ListManager, 
        'NotFound' : NotFound, 
        'StartPage' : StartPage, 
        'TemplateManager' : TemplateManager
    },
    computed: {
        component() {
            return this.$root.ViewComponent;
        }
    }
    
}
</script>

<style>

</style>