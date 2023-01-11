# Publish life cycle


## Update routing
Tools -> Version changer
```
git add *.csproj && git add *AssemblyInfo.cs
git commit -m "Updated version to v0.x.xxx"
git commit -m "#Version update to v0.x.xxx"
```

## Tag routing
```
git tag -a v0.x.xxx <commit_hash>
git push all --tags
```

## Branche routing
- develop -> preview: `git switch preview && git merge develop && git push all preview && git switch develop && git bra && git st`
- bagger -> preview:  `git switch bagger && git pull all bagger && git switch preview && git merge bagger && git bra && git st`
- preview -> develop: `git switch develop && git merge preview && git bra && git st`

## Tuesday Release-version
Switch on `preview` branch
```
cd C:\DevSource\Kolbasa-Git\VS.WeightService\
git checkout preview
```
Make changes
```
git add <files>
git commit -m "Release feature updates"
```
Switch on `main` branch
```
git checkout main
git merge preview
```
Publish BlazorDeviceControl dev-release: ```https://device-control-dev.kolbasa-vs.local/```
Publish BlazorDeviceControl prod-release: ```https://device-control.kolbasa-vs.local/```
Publish ScalesUI release: ```\\palych\Install\VSSoft\Scales-3-Release\```


## Friday Preview-version
Switch on `develop` branch
```
cd C:\DevSource\Kolbasa-Git\VS.WeightService\
git checkout preview
```
Make changes
```
git add <files>
git commit -m "Preview feature updates"
```
Publish BlazorDeviceControl dev-preview: ```https://device-control-dev-preview.kolbasa-vs.local/```
Publish BlazorDeviceControl prod-preview: ```https://device-control-prod-preview.kolbasa-vs.local/```
Publish ScalesUI test: ```\\palych\Install\VSSoft\Scales-1-Test\```
Publish ScalesUI preview: ```\\palych\Install\VSSoft\Scales-2-Preview\```


## Other days Develop-version
Switch on `preview` branch
```
cd C:\DevSource\Kolbasa-Git\VS.WeightService\
git checkout preview
```
Make changes
```
git add <files>
git commit -m "Develop feature updates"
```
Publish BlazorDeviceControl dev-preview: ```https://device-control-dev-preview.kolbasa-vs.local/```
Publish BlazorDeviceControl prod-preview: ```https://device-control-prod-preview.kolbasa-vs.local/```
Publish ScalesUI develop: ```\\palych\Install\VSSoft\Scales-1-Develop\```
