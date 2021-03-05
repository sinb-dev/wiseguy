import Vue from 'vue'
import EvaluationForm from './pages/EvaluationForm.vue'
import TemplateManager from './pages/TemplateManager.vue'
import NotFound from './pages/NotFound.vue'
import wb from "./registerServiceWorker";
Vue.prototype.$workbox = wb;
Vue.config.productionTip = false


const LandingPage = EvaluationForm;

const routes = {
  '^/$' : LandingPage,
  '^/eval/[a-z]+/$' : EvaluationForm,
  '^/tpl/$' : TemplateManager,
  '^/tpl/[a-z]./$' : TemplateManager
}
const server = 'https://monster.hoxer.net:5001/';
const axios = require("axios");
const app = new Vue({
  data : {
    currentRoute : document.location.hash.substr(1)
  },
  methods : {
    server(request) {
      return server+request;
    },
    post(path,data) {
      
      return axios.post(server+path, data)
    },
    get(path) {
      return axios.get(server+path)
    }
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
     
      return c(this.ViewComponent);
    }
    return c(NotFound);
  },
  created() {
    if (this.$workbox) {
      this.$workbox.addEventListener("waiting", () => {
        this.showUpdateUI = true;
      });
    }
  }
}).$mount('#app')
console.log(app.currentRoute);
window.onhashchange = function() {
  app.currentRoute = document.location.hash.substr(1)
}
