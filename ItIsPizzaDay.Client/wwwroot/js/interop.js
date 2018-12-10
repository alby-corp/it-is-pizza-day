(function() {
    window.interop = {
        dom: {
            scrollToBottom: function(element){
                element.scrollTop = element.scrollHeight;
            },
            getValue: function(element){
                return element.value;
            }
        }
    }
})();