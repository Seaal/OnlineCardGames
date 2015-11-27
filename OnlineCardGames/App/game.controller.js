(function() {

    angular
        .module("onlineCardGames")
        .controller("gameController", GameController);

    function GameController($stateParams, pushService) {
        var vm = this;
        vm.id = parseInt($stateParams.id);
        vm.messages = [];
        vm.me = {};

        pushService.on("sendGameMessage", function(message) {
            vm.messages.push(message);
        });

        pushService.initialise().then(function () {
            pushService.joinGame(vm.id).then(function(player) {
                vm.me = player;
            });
        });
    }

})();