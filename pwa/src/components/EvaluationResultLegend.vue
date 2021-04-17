<template>
<span>
    <h1>{{symbol}}</h1>
    <div>{{date_formatted}}</div>
    <div>{{time_formatted}}</div>
    
</span>
</template>

<script>
export default {
    props : {
        symbol : String,
        date : Date
    },
    computed : {
        date_formatted() {
            return this.date.getDate()+"-"+this.date.getMonth()+"-"+this.date.getFullYear();
        },
        time_formatted() {
            var hrs = this.date.getHours();
            if (hrs < 10) hrs = "0"+hrs;
            var mins = this.date.getMinutes();
            if (mins < 10) mins = "0"+mins;
            return "kl. "+hrs+":"+mins;

        }
    }
}
</script>

<style>
h1 {
    margin:0px 0px 5px 0px;

    padding:0px;
}
span {
    display:inline-block;
    text-align:center;
    border: 1px #333 solid ;
    border-radius:5px;
    margin:5px;
    padding:8px;
}
</style>