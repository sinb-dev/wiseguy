import Vue from 'vue'
import NotFound from './pages/NotFound.vue'
import wb from "./registerServiceWorker";
import "./components/PageWrapper.vue";

//Full monty
/*import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
Vue.use(VueMaterial)*/

// Component wise
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'

import PouchDB from 'pouchdb';

import { MdDialog, MdButton, MdField, MdContent, MdSnackbar, MdCard, MdEmptyState } from 'vue-material/dist/components'
Vue.use(MdDialog);
Vue.use(MdButton);
Vue.use(MdField);
Vue.use(MdContent);
Vue.use(MdSnackbar);
Vue.use(MdCard);
Vue.use(MdEmptyState);
//Use some interesting new designs
import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
//import 'vue-material/dist/theme/default-dark.css' // This line here
import PageWrapper from './components/PageWrapper.vue';

Vue.use(VueMaterial)

Vue.prototype.$workbox = wb;
Vue.config.productionTip = false

const routes = {
  '^\\/?$' : "StartPage",
  '^/eval/[a-zA-Z0-9]+\\/?$' : 'EvaluationForm',
  '^/tpl\\/?$' : 'TemplateManager',
  '^/tpl/[0-9]+\\/?$' : 'TemplateManager',
  '^/list\\/?$' : 'ListManager',
  '^/list/[0-9]+\\/?$' : 'ListManager',
  '^/issue/[0-9]+\\/?$' : 'IssuePage',
  '^/admin\\/?$' : 'Admin',
  '^/login\\/?$' : 'LoginPage',
}

const server = process.env.VUE_APP_WISEGUY_API_HOSTNAME+"/";
const axios = require("axios");
const DB = new PouchDB("wiseguy");
var loggedIn = false;
const app = new Vue({
  data : {
    currentRoute : "",
    pageData : null,
    recent : {_id : "recent", _rev : "", templates:[], lists: []},
    localIssues : {_id : "localIssues", _rev : "", issues : []},
    adminMode : false,
    UserMessage : false,
    DisplayMessage : false,
    
  },
  methods : {
    
    message(message) {
      this.UserMessage = message;
      this.DisplayMessage = true;
      setTimeout(() => {this.UserMessage = ""; this.DisplayMessage = false},4000)
    },
    server(request) {
      return server+request;
    },
    post(path,data) {
      
      return axios.post(server+path, data)
    },
    put(path,data) {
      
      return axios.put(server+path, data)
    },
    get(path) {
      return axios.get(server+path)
    },
    go(path, data) {
      console.log("Going from "+document.location.hash+" to #"+path);

      document.location.href = "#"+"/"+path.trim("/")+"/";
      this.pageData = data;
    },
    folder(idx) {
      idx = parseInt(idx);
      var folders = this.currentRoute.split("/");
      if (typeof(folders[idx]) != "undefined") {
        return folders[idx];
      }
    },
    onMaillistUpdate(maillist) {
      for (var i in this.recent.lists) {
        if (this.recent.lists[i].id == maillist.id) {
          this.recent.lists.splice(i,1);
        }
      }
      
      this.recent.lists.splice(0,0,maillist);
      if (this.recent.lists.length > 5)
        this.recent.lists = this.recent.lists.slice(0,5);

      var self = this;
      DB.put(this.recent).then(function() {
        self.loadRecent();
      });
    },
    onTemplateUpdate(template) {
      for (var i in this.recent.templates) {
        if (this.recent.templates[i].id == template.id) {
          this.recent.templates.splice(i,1);
        }
      }

      this.recent.templates.splice(0,0,template);
      
      if (this.recent.templates.length > 5)
        this.recent.templates = this.recent.templates.slice(0,5);
      var self = this;
      DB.put(this.recent).then(function() {
        self.loadRecent();
      })
    },
    loadRecent() {
      var self = this;
      DB.get("recent").then(function(recent) {
        self.recent = recent;
        self.recent._rev = recent._rev;
      }).catch(function(err) {
        if (err.status != 404) 
          console.log(err)
      });
    },
    onTemplateIssued(templateId, data) {
      var self = this;
      self.localIssues.issues.push(data);
      DB.put(self.localIssues).then(() => {
        self.loadLocalIssues();
      })
    },
    loadLocalIssues() {
      var self = this;
      DB.get("localIssues").then(issues => {
        
        if (issues) {
          self.localIssues = issues;
          self.localIssues._rev = issues._rev;

        } else
          self.localIssues = [];
  
      });
    },
    session(s) {
      let cookieStr = "";
      if (s)
        cookieStr="session="+s+";max-age=840;SameSite=Lax;secure"
      
      document.cookie = cookieStr;
      let session_id = document.cookie.split('; ').find(row => row.startsWith('session='))
      if (session_id)
        return session_id.split('=')[1];
        
      return "";
    },
    verifyLoggedIn() {
      if (this.session())
        return this.get("user/login/"+this.session())
          .then(() => loggedIn = true)
          .catch( () => loggedIn = false);
      else
        loggedIn = false;
    },
    isLoggedIn() {
      return loggedIn;
    },
    logUserIn(username,password,callback) {
      let data = new FormData();
      data.append("username",username);
      data.append("password",password);
      let self = this;
      this.post("user/login", data)
        .then((response)=> {
          self.message("Du er logget ind!");

          loggedIn = true;
          self.session(response.data);
          setTimeout(() => {
            if (callback !== undefined)
              callback() 
            self.go("admin");
            },1500)
        } )
        .catch(function (e) {
          console.error(e);
          self.sending = false;
          self.message("Kunne ikke logge dig ind. Kontrollér brugernavn og kodeord");
        });
    },
    logUserOut() {
      return this.get("user/logout/"+this.session())
    }
  },
  computed : {
    ViewComponent() {
      for (var k in routes) {
        var regex = new RegExp(k);
        if (regex.test(this.currentRoute)) {
          return routes[k];
        }
      }
      return NotFound
    }
  },
  render(c) {
    
    return c(PageWrapper);
    /*if (this.ViewComponent != NotFound) {
     
      return c(this.ViewComponent);
    }
    return c(NotFound);*/
  },
  created() {
    if (this.$workbox) {
      this.$workbox.addEventListener("waiting", () => {
        this.showUpdateUI = true;
      });
    }
    //var self = this;
    //var db = new PouchDB("wiseguy");
    this.verifyLoggedIn();
    this.loadRecent();
    this.loadLocalIssues();

    console.info("Remote API server: "+process.env.VUE_APP_WISEGUY_API_HOSTNAME)

    
  }
}).$mount('#app')
window.onhashchange = function() {
  app.currentRoute = document.location.hash.substr(1)
}

//window.onhashchange();
setInterval(() => document.getElementsByTagName("title").innerText = document.location.hash,16)