import Vue from 'vue'
import EvaluationForm from './pages/EvaluationForm.vue'
import NotFound from './pages/NotFound.vue'
import wb from "./registerServiceWorker";
Vue.prototype.$workbox = wb;
Vue.config.productionTip = false


const LandingPage = EvaluationForm;

const routes = {
  '^$' : LandingPage,
  '^/eval/[a-z]+/$' : EvaluationForm
}

const app = new Vue({
  data : {
    currentRoute : document.location.hash.substr(1),
  },
  computed : {
    
    ViewComponent() {
      for (var k in routes) {
        var regex = new RegExp(k);
        if (regex.test(this.currentRoute)) {
          return routes[k]
        }
      }
      return NotFound
    }
  },
  render(c) {
    if (this.ViewComponent != NotFound) {
      
      return c(LandingPage);
    }
    return c(this.ViewComponent);
  },
  created() {
    if (this.$workbox) {
      this.$workbox.addEventListener("waiting", () => {
        this.showUpdateUI = true;
      });
    }
  }
}).$mount('#app')

window.onhashchange = function() {
  app.currentRoute = document.location.hash.substr(1)
}
