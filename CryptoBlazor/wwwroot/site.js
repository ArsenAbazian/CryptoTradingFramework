function windowMinWidthMatchesQuery(dotNetHelper) {
    var pendingCall;
    var match = window.matchMedia("(min-width: 1200px)")
    handleMinWidthQuery(match).then(function () { match.addListener(handleMinWidthQuery) });
    function handleMinWidthQuery(queryMatch) {
        return (pendingCall || Promise.resolve(true))
            .then(function () {
                return pendingCall = new Promise(function (resolve, reject) {
                    dotNetHelper.invokeMethodAsync('OnWindowMinWidthQueryChanged', queryMatch.matches).then(resolve).catch(reject);
                });
            });
    }
}
