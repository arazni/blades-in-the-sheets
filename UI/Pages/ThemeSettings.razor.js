export async function fixBodyBackgroundColor() {
	const body = document.getElementsByTagName("body")[0];
	const themeElement = document.getElementsByTagName("main")[0].firstElementChild;
	const color = getComputedStyle(themeElement).getPropertyValue("--fill-color");
	body.style.backgroundColor = color;
}