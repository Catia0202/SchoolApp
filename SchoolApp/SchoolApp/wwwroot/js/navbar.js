// ---------Responsive-navbar-active-animation-----------
function test() {
	var tabsNewAnim = $('#navbarSupportedContent');
	var selectorNewAnim = $('#navbarSupportedContent').find('li').length;
	var activeItemNewAnim = tabsNewAnim.find('.active');
	var activeWidthNewAnimHeight = activeItemNewAnim.innerHeight();
	var activeWidthNewAnimWidth = activeItemNewAnim.innerWidth();
	var itemPosNewAnimTop = activeItemNewAnim.position();
	var itemPosNewAnimLeft = activeItemNewAnim.position();
	$(".hori-selector").css({
		"top": itemPosNewAnimTop.top + "px",
		"left": itemPosNewAnimLeft.left + "px",
		"height": activeWidthNewAnimHeight + "px",
		"width": activeWidthNewAnimWidth + "px"
	});
	$("#navbarSupportedContent").on("click", "li", function (e) {
		$('#navbarSupportedContent ul li').removeClass("active");
		$(this).addClass('active');
		var activeWidthNewAnimHeight = $(this).innerHeight();
		var activeWidthNewAnimWidth = $(this).innerWidth();
		var itemPosNewAnimTop = $(this).position();
		var itemPosNewAnimLeft = $(this).position();
		$(".hori-selector").css({
			"top": itemPosNewAnimTop.top + "px",
			"left": itemPosNewAnimLeft.left + "px",
			"height": activeWidthNewAnimHeight + "px",
			"width": activeWidthNewAnimWidth + "px"
		});
	});
}
$(document).ready(function () {
	setTimeout(function () { test(); });
});
$(window).on('resize', function () {
	setTimeout(function () { test(); }, 500);
});
$(".navbar-toggler").click(function () {
	$(".navbar-collapse").slideToggle(300);
	setTimeout(function () { test(); });
});
//$(".nav-item a").on("click", function () {
//	$(".nav-item a").removeClass("active");
//	$(this).addClass("active");
//});

var btnContainer = document.getElementById("nav-item");

// Get all buttons with class="btn" inside the container
var btns = btnContainer.getElementsByClassName("a");

// Loop through the buttons and add the active class to the current/clicked button
for (var i = 0; i < btns.length; i++) {
	btns[i].addEventListener("click", function () {
		var current = document.getElementsByClassName("active");
		current[0].className = current[0].className.replace(" active", "");
		this.className += " active";
	});
}
//// --------------add active class-on another-page move----------
//jQuery(document).ready(function ($) {
//	// Get current path and find target link
//	const path = document.querySelectorAll('.nav-link');

//	//// Account for home page with empty path
//	//if (path == '') {
//	//	path = 'index.html';
//	//}
//	console.log(path);
//	var target = $('#navbarSupportedContent ul li a[href="' + path + '"]');
//	// Add active class to target link
//	target.parent().addClass('active');
//});


//const links = document.querySelectorAll('.nav-link');

//if (links.length) {
//	links.forEach((link) => {
//		link.addEventListener('click', (e) => {
//			links.forEach((link) => {
//				link.classList.remove('active');
//			});
//			e.preventDefault();
//			link.classList.add('active');
//		});
//	});
//}


 ////Add active class on another page linked
 ////==========================================
 //$(window).on('load',function () {
 //    var current = location.pathname;
 //    console.log(current);
 //    $('#navbarSupportedContent ul li a').each(function(){
 //        var $this = $(this);
 //        // if the current path is like this link, make it active
 //        if($this.attr('href').indexOf(current) !== -1){
 //            /*$this.parent().addClass('active');*/
 //            //$this.parents('.menu-submenu').addClass('show-dropdown');
 //            $this.parents('.menu-submenu').parent().addClass('active');
 //        }else{
 //           /* $this.parent().removeClass('active');*/
 //        }
 //    })
 //});
