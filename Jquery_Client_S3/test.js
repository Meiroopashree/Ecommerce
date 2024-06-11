const puppeteer = require('puppeteer');
(async () => {
    const browser = await puppeteer.launch({
      headless: false,
      args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox']
    });
    const page = await browser.newPage();
    try{
      await page.goto('http://localhost:8081/');
      await page.setViewport({
        width:1200,
        height:1200,
      })
      const loadButtonExists = await page.evaluate(() => {
        return !!document.querySelector('#btnLoadData');
      });
  
      if (loadButtonExists) {
        console.log('TESTCASE_1:Load_Data_Button:success');
      } else {
        console.log('TESTCASE_1:Load_Data_Button:failure');
      }
    }
    catch(e){
      console.log('TESTCASE_1:Load_Data_Button:failure');
    }

    const page1 = await browser.newPage();

    try {           
        await page1.goto('http://localhost:8081');
        await page1.setViewport({
          width:1200,
          height:800,
        })
        await page1.click('#btnLoadData');

        // Handle the alert dialog
        page1.on('dialog', async (dialog) => {
          const dialogMessage = dialog.message();
          if (dialogMessage.includes('Data loaded successfully!')) {
            console.log('TESTCASE_2:Alert_Box_After_Load_Data_Button_Click:success');
          } else {
            console.log('TESTCASE_2:Alert_Box_After_Load_Data_Button_Click:failure');
          }
          await dialog.dismiss(); // Close the alert dialog
        });
        }
        catch(e){
        console.log('TESTCASE_2:Alert_Box_After_Load_Data_Button_Click:failure');
      }

    const page2 = await browser.newPage();
    try{
      await page2.goto('http://localhost:8081');
      await page2.setViewport({
        width:1200,
        height:800,
      })

      await page2.click('#btnLoadData');

    // Handle the alert dialog
    page2.on('dialog', async (dialog) => {
      const dialogMessage = dialog.message();
    //   console.log(dialogMessage);
      if (dialogMessage!='') {
        // console.log('TESTCASE: Alert Box After Load Data - Passed');
        await dialog.accept(); 
        await page2.waitForSelector('#dataContainer', { visible: true, timeout: 60000 }); // Increase timeout to 60 seconds
        const dataContainerText = await page2.$eval('#dataContainer', container => container.textContent.trim());
        if (dataContainerText!='') {
          console.log('TESTCASE_3:Load_Data_and_Verify:success');
        } else {
          console.log('TESTCASE_3:Load_Data_and_Verify:failure');
        }
      } else {
        console.log('TESTCASE_3:Load_Data_and_Verify:failure');
        await dialog.dismiss(); // Close the alert dialog
      }
    });
    }
    catch(e){
      console.log('TESTCASE_3:Load_Data_and_Verify:failure');
    } 

    const page3 = await browser.newPage();
    try{
      await page3.goto('http://localhost:8081');
      await page3.setViewport({
        width:1200,
        height:800,
      })
      const fadeButtonExists = await page.evaluate(() => {
        return !!document.querySelector('#btnFadeOddFruits');
      });
  
      if (fadeButtonExists) {
        console.log('TESTCASE_4:Fade_Odd_Fruits_Button:success');
      } else {
        console.log('TESTCASE_4:Fade_Odd_Fruits_Button:failure');
      }
    }
    catch(e){
      console.log('TESTCASE_4:Fade_Odd_Fruits_Button:failure');
    } 


  const page5 = await browser.newPage();
  try{
    await page5.goto('http://localhost:8081');
  await page5.setViewport({
    width: 1200,
    height: 800,
  });

  await page5.click('#btnFadeOddFruits');

    // Wait for fade animation to complete
    await page1.waitForTimeout(1000); // Adjust the timeout based on the animation duration

    const fadedFruitsCount = await page5.evaluate(() => {
      return document.querySelectorAll('#fruitList li[style*="display: none"]').length;
    });
    // console.log(fadedFruitsCount);

    if (fadedFruitsCount === 2) {
      console.log('TESTCASE_5:Fade_Odd_Fruits_and_Verify:success');
    } else {
      console.log('TESTCASE_5:Fade_Odd_Fruits_and_Verify:failure');
    }
  }
  catch(e){
    console.log('TESTCASE_5:Fade_Odd_Fruits_and_Verify:failure');
  }

  
  finally{
    await page.close();
    await page1.close();
    await page2.close();
    await page3.close();
    await page5.close();
    await browser.close();
  }
  
})();