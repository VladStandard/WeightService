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
- preview -> main:    `git switch main && git merge preview && git push all main && git switch preview && git bra && git st`
- develop -> main:    `git switch main && git merge develop && git push all main && git switch develop && git bra && git st`

## Release/Preview ScalesUI
Временное окно выпуска обновления ПО "Печать этикеток": 16.30 - 17.00.
Publish ScalesUI preview: `\\palych\Install\VSSoft\Scales-2-Preview\``
Publish ScalesUI release: `\\palych\Install\VSSoft\Scales-3-Release\`

## Release/Preview DeviceControl
Publish DeviceControl dev-preview:  `https://device-control-dev-preview.kolbasa-vs.local/`
Publish DeviceControl dev-release:  `https://device-control-dev.kolbasa-vs.local/`
Publish DeviceControl prod-preview: `https://device-control-prod-preview.kolbasa-vs.local/`
Publish DeviceControl prod-release: `https://device-control.kolbasa-vs.local/`
