export const navigateBackOrHome = () => {
  if (window.history.length > 2) {
    window.history.back();
    return;
  }
  window.location.href = window.location.origin + '/';
}

export const copyToClipboard = (text) =>
  navigator.clipboard.writeText(text)