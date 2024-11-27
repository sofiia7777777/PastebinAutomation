# PastebinAutomation
This project automates the process of creating a new paste on Pastebin with specific attributes.

## Steps

1. **Open Pastebin**  
   Open the [Pastebin website](https://pastebin.com/) or a similar service in any browser.

2. **Create a New Paste**  
   Create a new paste with the following attributes:
   - **Code**:
     ```bash
     git config --global user.name "New Sheriff in Town"
     git reset $(git commit-tree HEAD^{tree} -m "Legacy code")
     git push origin master --force
     ```
   - **Syntax Highlighting**: Select **"Bash"**.
   - **Paste Expiration**: Set to **"10 Minutes"**.
   - **Paste Name / Title**: Set the title to **"how to gain dominance among developers"**.

3. **Save Paste and Verify the Following**  
   After saving the paste, ensure that the following checks are completed:
   - The **browser page title** matches the **Paste Name / Title**.
   - The **syntax highlighting** is set to **Bash**.
   - The **code** matches exactly the one provided above in the instructions.

---

## Requirements

When performing the task, you must use the capabilities of Selenium WebDriver, a unit testing framework (NUnit), and the Page Object concept.

---
