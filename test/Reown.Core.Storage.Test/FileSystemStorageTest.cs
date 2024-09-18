using Reown.TestUtils;
using Xunit;

namespace Reown.Core.Storage.Test;

public class FileSystemStorageTest
{
    [Fact] [Trait("Category", "unit")]
    public async Task GetSetRemoveTest()
    {
        using var tempFolder = new TempFolder();
        var testDictStorage = new FileSystemStorage(Path.Combine(tempFolder.Folder.FullName, ".wctestdata"));
        await testDictStorage.Init();
        await testDictStorage.SetItem("somekey", "somevalue");
        Assert.Equal("somevalue", await testDictStorage.GetItem<string>("somekey"));
        await testDictStorage.RemoveItem("somekey");
        await Assert.ThrowsAsync<KeyNotFoundException>(() => testDictStorage.GetItem<string>("somekey"));
    }

    private static readonly string[] expected = new string[]
    {
        "addkey"
    };

    [Fact] [Trait("Category", "unit")]
    public async Task GetKeysTest()
    {
        using var tempFolder = new TempFolder();
        var testDictStorage = new FileSystemStorage(Path.Combine(tempFolder.Folder.FullName, ".wctestdata"));
        await testDictStorage.Init();
        await testDictStorage.Clear(); //Clear any persistant state
        await testDictStorage.SetItem("addkey", "testingvalue");
        Assert.Equal(expected, await testDictStorage.GetKeys());
    }

    [Fact] [Trait("Category", "unit")]
    public async Task GetEntriesTests()
    {
        using var tempFolder = new TempFolder();
        var testDictStorage = new FileSystemStorage(Path.Combine(tempFolder.Folder.FullName, ".wctestdata"));
        await testDictStorage.Init();
        await testDictStorage.Clear();
        await testDictStorage.SetItem("addkey", "testingvalue");
        Assert.Equal([
            "testingvalue"
        ], await testDictStorage.GetEntries());
        await testDictStorage.SetItem("newkey", 5);
        Assert.Equal(new int[]
        {
            5
        }, await testDictStorage.GetEntriesOfType<int>());
    }

    [Fact] [Trait("Category", "unit")]
    public async Task HasItemTest()
    {
        using var tempFolder = new TempFolder();
        var testDictStorage = new FileSystemStorage(Path.Combine(tempFolder.Folder.FullName, ".wctestdata"));
        await testDictStorage.Init();
        await testDictStorage.SetItem("checkedkey", "testingvalue");
        Assert.True(await testDictStorage.HasItem("checkedkey"));
    }
}