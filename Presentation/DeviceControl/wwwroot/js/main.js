window.goBack = () => {
  if (window.history.length > 2) {
    window.history.back();
    return;
  }
  window.location.href = window.location.origin + '/';
}
