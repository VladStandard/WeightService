export const animateDialogOpening = async(dialogId) => {
  let dialog = document.getElementById(dialogId)?.dialog;
  if (!dialog) return;

  dialog.classList.add('ease-in-out');
  let animation = dialog.animate([
    { opacity: '0', transform: 'scale(.95)' },
    { opacity: '1', transform: '' }
  ], {
    duration: 150,
  });
  await animation.finished;
}

export const animateDialogClosing = async(dialogId) => {
  let dialog = document.getElementById(dialogId)?.dialog;
  if (!dialog) return;

  dialog.classList.add('ease-in-out');
  let animation = dialog.animate([
    { opacity: '1', transform: '' },
    { opacity: '0', transform: 'scale(.8)' }
  ], {
    duration: 150,
  });
  dialog.style.opacity = '0.0';
  await animation.finished;
}