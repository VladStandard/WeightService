window.goBack = function () {
  if (window.history.length > 2) {
    window.history.back();
  } else {
    window.location.href = window.location.origin + '/';
  }
}
