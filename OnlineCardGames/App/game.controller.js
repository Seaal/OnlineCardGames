(function() {

    angular
        .module("onlineCardGames")
        .controller("gameController", GameController);

    function GameController($stateParams, pushService) {
        var vm = this;
        vm.id = $stateParams.id;
        vm.messages = [];
        vm.me = {};
        vm.hand = [];

        pushService.on("sendGameMessage", function(message) {
            vm.messages.push(message);
        });

        pushService.on("sendHand", function(hand) {
            vm.hand = hand;
        });

        pushService.initialise().then(function () {
            pushService.joinGame(vm.id).then(function(player) {
                vm.me = player;
            });
        });
    }

})();