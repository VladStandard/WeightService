/**
 * Switches the theme of the application based on the provided theme name.
 *
 * @param {string} theme - The name of the theme to switch to. Possible values are 'dark', 'light', or 'system'.
 * @return {void} This function does not return a value.
 */
window.switchTheme = (theme: string): void => {
  const root = document.documentElement
  let isDarkMode = theme === 'dark'

  if (theme === 'system') {
    localStorage.removeItem('color-theme')
    isDarkMode = window.matchMedia('(prefers-color-scheme: dark)').matches
  } else {
    localStorage.setItem('color-theme', theme)
  }

  root.classList.toggle('dark', isDarkMode)
}
