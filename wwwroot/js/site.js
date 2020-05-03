console.dir(document);

$(document).ready(function () {

    //TAKE
    // $('.lastShot_F').hide();
    // $('.takeShot_F').hide();
    // $('.fourSips_F').hide();
    // $('.threeSips_F').hide();
    // $('.twoSips_F').hide();
    // $('.oneSips_F').hide();
    // //GIVE
    // $('.giveShot_F').hide();
    // $('.giveFour_F').hide();
    // $('.giveThree_F').hide();
    // $('.giveTwo_F').hide();
    // $('.giveOne_F').hide();

    //Player Cards
    // $('.p1cd1_F').hide();
    // $('.p1cd2_F').hide();
    // $('.p1cd3_F').hide();
    // $('.p1cd4_F').hide();
    $('.back-card').hide();
    
    $('.card').click(function(){
        $(this).find('.front-card').toggle();
        $(this).find('.back-card').toggle();
    });


    // $('.p1rd1').click(function () {
    //     $(".p1cd1").hide();
    //     $(".p1cd1_F").show()
    // });
    // $('.p1rd2').click(function () {
    //     $(".p1cd2").hide();
    //     $(".p1cd2_F").show()
    // });
    // $('.p1rd3').click(function () {
    //     $(".p1cd3").hide();
    //     $(".p1cd3_F").show()
    // });
    // $('.p1rd4').click(function () {
    //     $(".p1cd4").hide();
    //     $(".p1cd4_F").show()
    // });


    // $('.butt1').click(function () {
        //     $("#card").flip({
            //         trigger: 'manual'
            //     });
            //     $("#card").flip('toggle');
            // });
            // $("#card").flip();

    // BUTTON LAST SHOT
    // $('.butt1').click(function () {
    //     $(".lastShot").hide();
    //     $(".lastShot_F").show()
    // });
            
    // // BUTTON TAKE SHOT
    // $('.butt2').click(function () {
    //     $(".takeShot").hide();
    //     $(".takeShot_F").show()
    // });
    // // BUTTON TAKE 4SIPS
    // $('.butt3').click(function () {
    //     $(".fourSips").hide();
    //     $(".fourSips_F").show()
    // });
    // // BUTTON TAKE 3SIPS
    // $('.butt4').click(function () {
    //     $(".threeSips").hide();
    //     $(".threeSips_F").show()
    // });
    // BUTTON TAKE 2SIPS
    // $('.butt5').click(function () {
    //     $(".twoSips").hide();
    //     $(".twoSips_F").show()
    // });
    // BUTTON TAKE 1SIP
    // $('.butt6').click(function () {
    //     $(".oneSips").hide();
    //     $(".oneSips_F").show()
    // });
    // BUTTON GIVE LAST SHOT
    // $('.butt7').click(function () {
    //     $(".giveShot").hide();
    //     $(".giveShot_F").show()
    // });
    // // BUTTON GIVE 4SIPS
    // $('.butt8').click(function () {
    //     $(".giveFour").hide();
    //     $(".giveFour_F").show()
    // });
    // // BUTTON GIVE 3SIPS
    // $('.butt9').click(function () {
    //     $(".giveThree").hide();
    //     $(".giveThree_F").show()
    // });
    // // BUTTON GIVE 2SIPS
    // $('.butt10').click(function () {
    //     $(".giveTwo").hide();
    //     $(".giveTwo_F").show()
    // });
    // // BUTTON GIVE 1SIPS
    // $('.butt11').click(function () {
    //     $(".giveOne").hide();
    //     $(".giveOne_F").show()
    // });


    // $('#butt1').click(function () {
    //     $('.lastShot',this).hide();
    //     $('lastShot_F',this).show();
    // });
});
