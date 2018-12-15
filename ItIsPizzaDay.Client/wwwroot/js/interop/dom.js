(function(self) {
    self.dom = {
            scrollToBottom: function(element){
                element.scrollTop = element.scrollHeight;
            },
            getValue: function(element){
                return element.value;
            }
    }
})(window.interop || (window.interop = {}));