<template>
<div>
    <div class="ui basic modal">
        <select v-model="maillistId"><option value="">Choose list</option><option v-for="list in lists"
            :key="list.id" :value="list.id">{{list.name}}</option></select>
        <button @click="issue">Issue</button>
    </div>
</div>
</template>

<script>
import $ from "jquery";

import '../../public/assets/semantic.js';
export default {
    props : {
        lists : Array,
        templateId : Number
    },
    data : function() {
        return {
            maillistId : 0
        }
    },
    methods : {
        issue() {
            var mid = parseInt(this.maillistId);
            var tid = parseInt(this.templateId);
            
            if (isNaN(mid) || mid == 0) return console.error("Cannot issue template to list. The mail list id is not a valid number");
            if (isNaN(tid) || tid == 0) return console.error("Cannot issue template to list. The template id is not a valid number");

            var data = new FormData();
            data.append("templateId", this.templateId);
            data.append("maillistId", this.maillistId)
            var self = this;
            this.$root
                .post("tpl/"+this.templateId+"/issue",data)
                .then(response => {
                    if (response.data == "NULL") {
                        console.error("Error occured while trying to issue template");
                    }
                    self.$emit("onIssued", response.data);
                    
                });
        },
        
    },
    mounted() {
        $(".ui.basic.modal").modal('show')
    }
}
</script>

<style>

</style>