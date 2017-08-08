
# Simple301 Fork by RY #

[![Build status](https://ci.appveyor.com/api/projects/status/p8nskqbofs0oh7um?svg=true)](https://ci.appveyor.com/project/giuunit/simple301)

Based on the Simple301 umbraco package. 

Changes: 
- No more regex (not User-friendly enough for clients)
- Domain column & Logic added
- No more caching - hard to manage with domains 

Dont forget to edit your dashboard.config file:

```
 <section alias="StartupDashboardSection">
    <access>
      <deny>translator</deny>
    </access>
    <areas>
      <area>content</area>
    </areas>
    <tab caption="Get Started">
      <access>
        <grant>admin</grant>
      </access>

      <control showOnce="true" addPanel="true" panelCaption="">
        views/dashboard/default/startupdashboardintro.html
      </control>
    </tab>
    <tab caption="Url Tracker">
      <control addPanel="true">~/App_Plugins/Simple301/app.html</control>
    </tab>
  </section>
  ```
