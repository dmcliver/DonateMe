var AjaxResponseMock = (function() {

    "use strict";

    var self = this;

    var notifyDoneCaptor;
    var notifyFailCaptor;
    var notifyCompleteCaptor;

    self.getDoneArgument = function() {
        return notifyDoneCaptor;
    }
    self.getFailArgument = function() {
        return notifyFailCaptor;
    }
    self.getCompleteArgument = function() {
        return notifyCompleteCaptor;
    }
    self.done = function(notifyDone) {
        notifyDoneCaptor = notifyDone;
        return self;
    }
    self.fail = function(notifyFail) {
        notifyFailCaptor = notifyFail;
        return self;
    }
    self.always = function(notifyComplete) {
        notifyCompleteCaptor = notifyComplete;
        return self;
    }
});