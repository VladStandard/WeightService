# Publish lifecycle


## Tuesday Release

Switch on `preview` branch
```
cd C:\DevSource\Kolbasa-Git\VS.WeightService\
git checkout preview
```
Make changes
```
git commit -m "Feature updates"
```
Switch on `main` branch
```
git checkout main
git merge preview
```
Publish BlazorDeviceControl dev-release: `https://device-control-dev.kolbasa-vs.local/`
Publish BlazorDeviceControl prod-release: `https://device-control.kolbasa-vs.local/`
Publish ScalesUI release: `\\palych\Install\VSSoft\Scales-3-Release\`


## Friday Previews

Switch on `preview` branch
```
cd C:\DevSource\Kolbasa-Git\VS.WeightService\
git checkout preview
```
Make changes
```
git commit -m "Feature updates"
```
Publish BlazorDeviceControl dev-preview: `https://device-control-dev-preview.kolbasa-vs.local/`
Publish BlazorDeviceControl prod-preview: `https://device-control-prod-preview.kolbasa-vs.local/`
Publish ScalesUI test: `\\palych\Install\VSSoft\Scales-1-Test\`
Publish ScalesUI preview: `\\palych\Install\VSSoft\Scales-2-Preview\`
