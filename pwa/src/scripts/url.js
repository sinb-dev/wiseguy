
let url = document.location.hash.substr(1);
if (url.substr(-1,1) != "/") url += "/";
let folders = url.split("/");

folders.shift();
folders.pop()

function workingDirectory() {
    return folders[folders.length-1];
}
function parent() {
    return folders[folders.length-2]
}
function ancestor(number) {
    if (isNaN(number)) number = 1;
    return folders[folders.length-number-1]
}

export default {
    workingDirectory : workingDirectory,
    workDir : workingDirectory,
    parent : parent,
    up : parent,
    ancestor : ancestor,
    url() {
        return url;
    }
}