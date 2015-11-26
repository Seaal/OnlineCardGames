(function() {

    angular
        .module("onlineCardGames")
        .config(routes);

    function routes($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "App/home.view.html",
                controller: "homeController as home"
            });
    }

})();