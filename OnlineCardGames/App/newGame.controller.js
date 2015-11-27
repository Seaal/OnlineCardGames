(function() {

    angular
        .module("onlineCardGames")
        .controller("newGameController", NewGameController);

    function NewGameController() {
        var vm = this;

        vm.name = "";
        vm.chipCount = 1500;
        vm.maxPlayers = 6;
        vm.createGame = createGame;

        function createGame() {
            console.log("I was clicked!");
        }
    }

})();