function changeLanguageHeader() {
    var http = new XMLHttpRequest();
    var element = document.querySelector("body header nav select");
    element.addEventListener("change", (event) => {
        http.open('GET', window.location.href);
        http.setRequestHeader("Accept-Language", element.value);
        http.send();
        
    });
}

changeLanguageHeader();