console.log("start")
import Vue from 'vue'
import StartPage from "./pages/StartPage.vue";
import EvaluationForm from './pages/EvaluationForm.vue'
import TemplateManager from './pages/TemplateManager.vue'
import ListManager from './pages/ListManager.vue'
import NotFound from './pages/NotFound.vue'
import wb from "./registerServiceWorker";
import IssuePage from "./pages/IssuePage.vue";

//Full monty
/*import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
Vue.use(VueMaterial)*/

// Component wise
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'

import { MdDialog, MdButton, MdField, MdContent } from 'vue-material/dist/components'
Vue.use(MdDialog);
Vue.use(MdButton);
Vue.use(MdField);
Vue.use(MdContent);

Vue.prototype.$workbox = wb;
Vue.config.productionTip = false

const LandingPage = StartPage;
const routes = {
  '^/$' : LandingPage,
  '^/eval/[a-zA-Z0-9]+/$' : EvaluationForm,
  '^/tpl/$' : TemplateManager,
  '^/tpl/[0-9]+/$' : TemplateManager,
  '^/list/$' : ListManager,
  '^/list/[0-9]+/$' : ListManager,
  '^/issue/[0-9]+/$' : IssuePage,

}
const server = 'https://localhost:5001/';
const axios = require("axios");

const app = new Vue({
  data : {
    currentRoute : "",
    pageData : null,
    
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
    },
    go(path, data) {
      document.location.hash = "#"+path;
      this.pageData = data;
    },
    folder(idx) {
      idx = parseInt(idx);
      var folders = this.currentRoute.split("/");
      if (typeof(folders[idx]) != "undefined") {
        return folders[idx];
      }
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
window.onhashchange = function() {
  app.currentRoute = document.location.hash.substr(1)
  if (app.currentRoute.substr(-1,1) != "/")
    document.location.hash += "/";
}
window.onhashchange();
