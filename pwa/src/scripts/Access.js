var table = {}
export default {

    add(newAccessString, mail) {
        if (typeof newAccessString != "string" || newAccessString == "")
            return;

        if (!mail) mail = "";
        this.load();
        table[newAccessString] = mail;
        this.save();
    },
    save() {
        localStorage.setItem("access",this.tostring());
    },
    load() {
        let accessStr = localStorage.getItem("access");
        if (! accessStr) return;
        let params = accessStr.split("&");
        table = {}
        for (let i in params) {
            let val = params[i].split("=");
            let email = val[1];
            let access = val[0]; 
            table[access] = email;
        }
    },
    get_accessTokens() {
        var list = [];
        this.load();
        for(var token in table) {
            list.push(token);
        }
        return list;

    },
    tostring() {
        var arr = [];
        for (let access in table) {
            if (access)
                arr.push ( access+"="+table[access]);
        }
        return arr.join("&");
    }

}